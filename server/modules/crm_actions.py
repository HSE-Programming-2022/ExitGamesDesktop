import copy
import json
import requests
from requests.auth import HTTPBasicAuth
import datetime


class CRM:
    def __init__(self):
        self._username = 'animator'
        self._password = 'Cj7ITn!PHNWMV0KIqZ@bVQf4ziJg21wH'
        self._auth = HTTPBasicAuth(self._username, self._password)
        self._url_games = 'https://crm.exitgames.ru/animator-api/games'
        self._url_corp_packs = 'https://crm.exitgames.ru/animator-api/corp-packs'
        self._url_tables = 'https://crm.exitgames.ru/animator-api/table'

    def _get_games(self, date):
        params = (
            ('start', f'{date.strftime("%d.%m.%Y")}'),
            ('end', f'{date.strftime("%d.%m.%Y")}'),
            ('status', 3)
        )

        resp = requests.get(self._url_games, auth=self._auth, params=params)
        resp = json.loads(resp.content)
        games3 = resp["games"]

        params = (
            ('start', f'{date.strftime("%d.%m.%Y")}'),
            ('end', f'{date.strftime("%d.%m.%Y")}'),
            ('status', 5)
        )

        resp = requests.get(self._url_games, auth=self._auth, params=params)
        resp = json.loads(resp.content)
        games5 = resp["games"]

        games = games3 + games5
        return games

    def _get_corp_packs(self, date):
        params = (
            ('start', f'{date.strftime("%d.%m.%Y")}'),
            ('end', f'{date.strftime("%d.%m.%Y")}'),
            ('status', 1)
        )

        resp = requests.get(self._url_corp_packs, auth=self._auth, params=params)
        resp = json.loads(resp.content)
        corp_packs1 = resp["corp-packs"]

        params = (
            ('start', f'{date.strftime("%d.%m.%Y")}'),
            ('end', f'{date.strftime("%d.%m.%Y")}'),
            ('status', 2)
        )

        resp = requests.get(self._url_corp_packs, auth=self._auth, params=params)
        resp = json.loads(resp.content)
        corp_packs2 = resp["corp-packs"]

        params = (
            ('start', f'{date.strftime("%d.%m.%Y")}'),
            ('end', f'{date.strftime("%d.%m.%Y")}'),
            ('status', 4)
        )

        resp = requests.get(self._url_corp_packs, auth=self._auth, params=params)
        resp = json.loads(resp.content)
        corp_packs4 = resp["corp-packs"]

        corp_packs = corp_packs1 + corp_packs2 + corp_packs4
        return corp_packs

    def get_tables(self, date):
        params = (
            ('start', f'{date.strftime("%d.%m.%Y")}'),
            ('end', f'{date.strftime("%d.%m.%Y")}'),
            ('status', 1)
        )

        resp = requests.get(self._url_tables, auth=self._auth, params=params)
        resp = json.loads(resp.content)
        tables1 = resp["table"]

        params = (
            ('start', f'{date.strftime("%d.%m.%Y")}'),
            ('end', f'{date.strftime("%d.%m.%Y")}'),
            ('status', 2)
        )

        resp = requests.get(self._url_tables, auth=self._auth, params=params)
        resp = json.loads(resp.content)
        tables2 = resp["table"]

        params = (
            ('start', f'{date.strftime("%d.%m.%Y")}'),
            ('end', f'{date.strftime("%d.%m.%Y")}'),
            ('status', 4)
        )

        resp = requests.get(self._url_tables, auth=self._auth, params=params)
        resp = json.loads(resp.content)
        tables4 = resp["table"]

        tables = tables1 + tables2 + tables4
        return tables

    def get_activities(self, date, table=False):
        raw_games = self._get_games(date)
        raw_corp_packs = self._get_corp_packs(date)
        tables = self.get_tables(date)

        games = []
        corp_packs = []

        for i in range(0, len(raw_games)):
            if raw_games[i]["facttime"] is None:
                games.append({"date": datetime.datetime.strptime(raw_games[i]["startdatetime"], '%d.%m.%Y %H:%M') + datetime.timedelta(hours=3),
                              "name": raw_games[i]["name"],
                              "animator": raw_games[i]["animator"],
                              "number of children": raw_games[i]["number of children"]
                              }
                             )
            else:
                games.append({"date": datetime.datetime.strptime(raw_games[i]["facttime"], '%d.%m.%Y %H:%M') + datetime.timedelta(hours=3),
                              "name": raw_games[i]["name"],
                              "animator": raw_games[i]["animator"],
                              "number of children": raw_games[i]["number of children"]
                              }
                             )

        for i in range(0, len(raw_corp_packs)):
            if isinstance(raw_corp_packs[i]["zones"], dict):
                zones = list(raw_corp_packs[i]["zones"].values())
            else:
                zones = raw_corp_packs[i]["zones"]
            for j in range(0, len(zones)):
                for k in range(0, len(tables)):
                    if zones[j]["name"] == tables[k]["Зона"] and zones[j]["time"] == tables[k]["Время"]:
                        zones[j]["table"] = tables[k]
            if isinstance(raw_corp_packs[i]["games"], dict):
                list_games = list(raw_corp_packs[i]["games"].values())
            else:
                list_games = raw_corp_packs[i]["games"]
            if isinstance(raw_corp_packs[i]["addservices"], dict):
                addservices = list(raw_corp_packs[i]["addservices"].values())
            else:
                addservices = raw_corp_packs[i]["addservices"]
            to_delete = []
            for j in range(0, len(addservices)):
                if addservices[j]["time"] is None and addservices[j]["name"] != "Мастер-класс":
                    to_delete.append(addservices[j])
            for j in range(0, len(to_delete)):
                addservices.remove(to_delete[j])
            if len(zones) != 0 or len(addservices) != 0:
                corp_packs.append({"number of children": raw_corp_packs[i]["number of children"],
                                   "zones": zones,
                                   "games": list_games,
                                   "addservices": addservices
                                   }
                                  )
        for i in range(0, len(corp_packs)):
            for j in range(len(corp_packs[i]['zones'])):
                if corp_packs[i]['zones'][j].get('table') is not None:
                    for k in range(len(corp_packs[i]['zones'])):
                        corp_packs[i]['zones'][k]['table'] = copy.copy(corp_packs[i]['zones'][j].get('table'))

        for i in range(0, len(corp_packs)):
            for j in range(len(corp_packs[i]['zones'])):
                corp_packs[i]['zones'][j]['table']['Зона'] = corp_packs[i]['zones'][j]['name']
        if not table:
            return games, corp_packs
        else:
            tables = []
            for i in range(0, len(corp_packs)):
                for j in range(len(corp_packs[i]['zones'])):
                    tables.append(corp_packs[i]['zones'][j]['table'])
            return tables

    def get_parsed_tables(self, date):
        g, c = CRM().get_activities(date)
        tables = []
        for cp in c:
            for zone in cp.get('zones'):
                zone.get('table')['Время'] = zone.get('time')
                try:
                    start = datetime.datetime.strptime(zone.get('time').split(' - ')[0], '%H:%M')
                    finish = datetime.datetime.strptime(zone.get('time').split(' - ')[1], '%H:%M')
                    if (finish - start).seconds > 900:
                        tables.append(zone.get('table'))
                except:
                    pass
        return tables
