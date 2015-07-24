from flask import Flask, request
import json
import logging
from uuid import uuid1

app = Flask(__name__)
log = app.logger

userDB = []
statusDB = []

@app.route('/twittusers')
def getallusers():
        return json.dumps(userDB)



@app.route('/twittusers/<uid>')
def getuser(uid):
        user = filter(lambda o : o['id'] == uid, userDB)[0]
        return user 


@app.route('/twittusers/screenname/<name>')
def getuserbyscreenname(name):
        user = filter(lambda o : o['screenName'] == name, userDB)[0]
        return json.dumps(user) 


@app.route('/twittusers/<uid>/status/')
def getuserstatuslist(uid):
        statuslist = filter(lambda o : o['userid'] == uid, statusDB) 
        return json.dumps(statuslist)
        

@app.route('/twittusers', methods=['POST'])
def adduser():
        r = request.get_json(force=True)
        log.debug(r)
        userDB.append({
                'id': r['id'], 
                'name': r['name'], 
                'screenName': r['screenName'], 
                'location':r['location'], 
                'description': r['description']})
        return "OK", 200 


@app.route('/twittusers', methods=['PUT'])
def modifyuser():
        r = request.json
        userDB.append(json.loads(r.form))
        return "OK", 200 




@app.route('/status')
def getallstatuses():
        return json.dumps(statusDB)


@app.route('/status/<uid>')
def getstatus(uid):
        status = filter(lambda o : o['id'] == uid, statusDB)[0]
        return json.dumps(status)


@app.route('/status/<uid>/text', methods=['PUT'])
def modifytext(uid):
        status = filter(lambda o : o['id'] == uid, statusDB)[0]
        status['text'] = request.data
        return "OK", 200


@app.route('/status', methods=['POST'])
def addstatus():
        r = request.get_json(force=True)
        log.debug(r)
        statusDB.append({
                'userid': r['userid'], 
                'text': r['text'], 
                'id': r['id'], 
                'created_at':r['created_at']})
        return "OK", 200 


@app.route('/status/<uid>', methods=["DELETE"])
def removestatus(uid):
        status = filter(lambda o : o['id'] == uid, statusDB)[0]
        statusDB.remove(status) 
        return "OK", 200 


def makeuserdummy():

        userDB.append({
                'id': 'userID1', 
                'name': 'name1', 
                'screenName': 'screenname1', 
                'location':'location', 
                'description': 'description...'})

        userDB.append({
                'id': 'userID2', 
                'name': 'name2', 
                'screenName': 'screenname2', 
                'location':'location', 
                'description': 'description...'})
        

def makestatusdummy():
        
        statusDB.append( {
                'userid':'userID1', 'text': 'hello from status 1', 
                'id': 'theid1', 'created_at': '12-12-2012'})
        statusDB.append( {
                'userid':'userID1', 'text': 'hello from status 2', 
                'id': 'theid2', 'created_at': '12-12-2013'})
        statusDB.append( {
                'userid':'userID2', 'text': 'hello from status 3', 
                'id': 'theid3', 'created_at': '12-12-2012'})
        statusDB.append( {
                'userid':'userID2', 'text': 'hello from status 4', 
                'id': 'theid4', 'created_at': '12-12-2013'})


if __name__ == "__main__":
        app.debug = True
        makeuserdummy()
        makestatusdummy()
        app.run('0.0.0.0', port=8080)









