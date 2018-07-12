class FileTypesProvider():

    def lbph(self):
        return 1

    def eigen(self):
        return 2

    def fisher(self):
        return 3

    def get_file_type_id(self, file_name: str):
        if "lbph" in file_name.lower():
            return self.lbph()
        elif "eigen" in file_name.lower():
            return self.eigen()
        elif "fisher" in file_name.lower():
            return self.fisher()
