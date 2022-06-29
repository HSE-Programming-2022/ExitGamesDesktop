import datetime

from models.LuftTime import LuftTime


class Animator:
    def __init__(self, name='', activities=None):
        if activities is None:
            self.activities = []
        else:
            self.activities = activities
        self.name = name
        self.total_salary = 0

    def add_luft(self, start, finish):
        if start.hour == 9 and start.minute == 0 and finish.hour == 23 and finish.minute == 0:
            return
        if start.hour == 9 and start.minute == 0:
            self.activities.append(
                LuftTime(
                    finish,
                    datetime.time(23, 0)
                )
            )
        if finish.hour == 23 and finish.minute == 0:
            self.activities.append(
                LuftTime(
                    datetime.time(9, 0),
                    start
                )
            )
        if finish.hour != 23 and start.hour == 9:
            if start.minute != 0:
                self.activities.append(
                    LuftTime(
                        datetime.time(9, 0),
                        start
                    )
                )
                self.activities.append(
                    LuftTime(
                        finish,
                        datetime.time(23, 0)
                    )
                )
        elif finish.hour != 23 and start.hour != 9:
            self.activities.append(
                LuftTime(
                    datetime.time(9, 0),
                    start
                )
            )
            self.activities.append(
                LuftTime(
                    finish,
                    datetime.time(23, 0)
                )
            )

    def total_salary_reload(self):
        self.total_salary = sum(act.price for act in self.activities)
