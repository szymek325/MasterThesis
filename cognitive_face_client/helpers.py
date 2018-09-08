from threading import Thread

from PIL import Image

IMAGE_WILDCARD = 'Image files (*.jpg, *.png)|*.jpg; *.png'
INNER_PANEL_WIDTH = 710
MAX_IMAGE_SIZE = 300
MAX_THUMBNAIL_SIZE = 75
SUBSCRIPTION_KEY_FILENAME = 'Subscription.txt'
ENDPOINT_FILENAME = 'Endpoint.txt'
ORIENTATION_TAG = 274

def scale_image(img, size=MAX_IMAGE_SIZE):
    """Scale the wx.Image."""
    width = img.GetWidth()
    height = img.GetHeight()
    if width > height:
        new_width = size
        new_height = size * height / width
    else:
        new_height = size
        new_width = size * width / height
    img = img.Scale(new_width, new_height)
    return img

def async(func):
    """Async wrapper."""

    def wrapper(*args, **kwargs):
        """Async wrapper."""
        thr = Thread(target=func, args=args, kwargs=kwargs)
        thr.start()

    return wrapper