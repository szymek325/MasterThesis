import os


def get_file_size(path_to_file):
    size = os.path.getsize(path_to_file)
    return get_human_readable_file_size(size)


def get_human_readable_file_size(size, precision=2):
    suffixes = ['B', 'KB', 'MB', 'GB', 'TB']
    suffixIndex = 0
    while size > 1024:
        suffixIndex += 1  # increment the index of the suffix
        size = size / 1024.0  # apply the division
    return "%.*f %d" % (precision, size, suffixes[suffixIndex])
