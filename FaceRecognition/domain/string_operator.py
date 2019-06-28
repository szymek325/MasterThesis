class StringOperator():

    def find_between(self, s, first, last):
        try:
            start = s.index(first) + len(first)
            end = s.index(last, start)
            return s[start:end]
        except ValueError:
            return ""

    def find_between_r(self, s, first, last):
        try:
            start = s.rindex(first) + len(first)
            end = s.rindex(last, start)
            return s[start:end]
        except ValueError:
            return ""

    def get_file_name_from_path(self, file_path):
        try:
            file_name = file_path.split('\\')[-1]
            return file_name
        except ValueError:
            return ""
