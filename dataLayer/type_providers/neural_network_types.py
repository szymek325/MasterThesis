from configuration_global.logger_factory import LoggerFactory


class NeuralNetworkTypes:
    def __init__(self):
        self.logger = LoggerFactory()

    @property
    def lbph(self):
        return "LBPH"

    @property
    def eigen(self):
        return "Eigen"

    @property
    def fisher(self):
        return "Fisher"

    @property
    def lbph_id(self):
        return 1

    @property
    def eigen_id(self):
        return 2

    @property
    def fisher_id(self):
        return 3

    def get_type_id(self, nn_type_name: str):
        if nn_type_name.lower() == self.lbph.lower():
            return self.lbph_id
        elif nn_type_name.lower() == self.eigen.lower():
            return self.eigen_id
        elif nn_type_name.lower() == self.fisher.lower():
            return self.fisher_id
        else:
            raise Exception(f"Wrong nn_type_name. Cant find a match for : {nn_type_name}")

    def get_type_name(self, nn_type_id:int):
        if nn_type_id is self.lbph_id:
            return self.lbph
        elif nn_type_id is self.fisher_id:
            return self.fisher
        elif nn_type_id is self.eigen_id:
            return self.eigen
        else:
            self.logger.error(f"Unknown nn_type_id: {nn_type_id}")
            return "Unknown"
