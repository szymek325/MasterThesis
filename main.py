import json
import cv2
import time
import imutils
import datetime

from logger.logger_factory import LoggerFactory

configuration = json.load(open("./config.json"))
logger = LoggerFactory()

if configuration["piCameraUsed"]:
    print("Picamera cannot be launch on windows")
    raise Exception("Picamera cannot be launch on windows")
# COnfiguration for Raspberry
# camera = PiCamera()
# camera.resolution = tuple(conf["resolution"])
# camera.framerate = conf["fps"]
# rawCapture = PiRGBArray(camera, size=tuple(conf["resolution"]))
else:
    camera = cv2.VideoCapture(configuration["cameraPort"])

time.sleep(configuration["camera_warmup_time"])
averageFrame = None
motionCounter = 0
lastUploaded = datetime.datetime.now()

while True:
    grabbed, frame = camera.read()
    if not grabbed:
        logger.error("Camera didn't grab a frame, raising error")
        break
    timestamp = datetime.datetime.now()
    # resize the frame, convert it to grayscale, and blur it
    frame = imutils.resize(frame, width=500)
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    gray = cv2.GaussianBlur(gray, (21, 21), 0)

    if averageFrame is None:
        averageFrame = gray
        continue

    # accumulate the weighted average between the current frame and
    # previous frames, then compute the difference between the current
    # frame and running average
    cv2.accumulateWeighted(gray, averageFrame, 0.5)
    frameDelta = cv2.absdiff(gray, cv2.convertScaleAbs(averageFrame))

    # threshold the delta image, dilate the thresholded image to fill
    # in holes, then find contours on thresholded image
    thresh = cv2.threshold(frameDelta, configuration["delta_thresh"], 255, cv2.THRESH_BINARY)[1]
    thresh = cv2.dilate(thresh, None, iterations=2)
    _, contours, _ = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
    contours = contours[0] if imutils.is_cv2() else contours[1]

    for c in contours:
        # if the contour is too small, ignore it
        if cv2.contourArea(c) < configuration["min_area"]:
            continue

        # compute the bounding box for the contour, draw it on the frame,
        # and update the text
        (x, y, w, h) = cv2.boundingRect(c)
        cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
        text = "Occupied"

        # draw the text and timestamp on the frame
        ts = timestamp.strftime("%A %d %B %Y %I:%M:%S%p")
        cv2.putText(frame, "Room Status: {}".format(text), (10, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 2)
        cv2.putText(frame, ts, (10, frame.shape[0] - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.35, (0, 0, 255), 1)

        # check to see if the room is occupied
        if text == "Occupied":
            # check to see if enough time has passed between uploads
            if (timestamp - lastUploaded).seconds >= 2.5:
                # increment the motion counter
                motionCounter += 1

                # check to see if the number of frames with consistent motion is
                # high enough
                if motionCounter >= configuration["min_motion_frames"]:
                    print("MOVEMENT SAVED")
        else:
            motionCounter=0

        if configuration["show_video"]:
            # display the security feed
            cv2.imshow("Security Feed", frame)
            key = cv2.waitKey(1) & 0xFF

            if key == ord("q"):
                break


camera.release()
cv2.destroyAllWindows()
