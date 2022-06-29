class Zone:
    def __init__(self, name, start, finish, hours_350, hours_200, mk, table=None):
        self.name = name
        self.start = start
        self.finish = finish
        self.hours_350 = hours_350
        self.hours_200 = hours_200
        self.hours_500 = ((finish - start).seconds / 3600 - hours_350 - hours_200)
        self.mk = mk
        self.price = int(hours_200 * 200 + hours_350 * 350 + self.hours_500 * 500 + mk * 500)
        self.table = table
        self.colour_info = ''
        self.cell_text = ''

    def reload_price(self):
        self.price = int(self.hours_200 * 200 + self.hours_350 * 350 + self.hours_500 * 500 + self.mk * 500)
