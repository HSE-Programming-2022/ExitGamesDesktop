class Game:
    def __init__(self, name, start, finish, have_helper=False):
        self.name = name
        self.start = start
        self.finish = finish
        self.price = 350
        self.have_helper = have_helper
        self.colour_info = ''
        self.cell_text = ''
