import os
from flask import Flask, request, send_file
from flask_httpauth import HTTPBasicAuth
from werkzeug.security import generate_password_hash, check_password_hash
from threading import Thread
import datetime
import requests
from requests.structures import CaseInsensitiveDict
from modules.Schedule import Schedule


app = Flask('')
sch = Schedule()
auth = HTTPBasicAuth()

users = {
    "project": generate_password_hash("exitgames")
}

offset = datetime.timedelta(hours=3)
tz = datetime.timezone(offset, name='МСК')


def check_login(login, password):
    try:
        url = os.environ['url_login_CRM']
    
        headers = CaseInsensitiveDict()
        headers["X-Requested-With"] = "XMLHttpRequest"
        headers["Content-Type"] = "application/x-www-form-urlencoded"
    
        data = f"LoginForm[username]={login}&LoginForm[password]={password}"
    
        resp = requests.post(url, headers=headers, data=data)
    
        if resp.content.decode() == '[]':
            return True
        else:
            return False
    except Exception:
        return False


@auth.verify_password
def verify_password(username, password):
    if username in users and \
            check_password_hash(users.get(username), password):
        return username


@app.route('/', methods=['GET'])
def main():
    return 'As-salamu alaykum!'


@app.route('/api/login', methods=['POST'])
@auth.login_required
def login():
    try:
        request_data = request.get_json()
        login = request_data.get('login')
        password = request_data.get('password')
        if login and password:
            if login == 'project' and password == 'exitgames':
                login = os.environ['login_CRM']
                password = os.environ['password_CRM']
            if check_login(login, password):
                return {'status': True}
            else:
                return {'status': False}
    except Exception:
        return {'status': 'Error'}


@app.route('/api/get_schedule', methods=['POST'])
@auth.login_required
def get_schedule():
    try:
        request_data = request.get_json()
        mode = request_data.get('mode')
        date = datetime.datetime.strptime(request_data.get('date'), '%d.%m.%Y')
        table_name = None
        if mode == 'weekends':
            timedelta_info = request_data.get('timedelta_info')
            table_name = sch.make_schedule_weekends(date, timedelta_info)
        elif mode == 'weekdays':
            table_name = sch.make_schedule_weekdays(date)
        return send_file(table_name)
    except Exception:
        return {'status': 'Error'}



def run():
    app.run(host="0.0.0.0", port=8000)


def keep_alive():
    server = Thread(target=run)
    server.start()


keep_alive()
while True:
    pass
