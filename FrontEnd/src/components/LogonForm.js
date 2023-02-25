import '../assets/UserForm.css';
import logonUser from '../hooks/FetchRequest/LogonUser';
import { tryStoreToken } from '../utilites/StorageService'
import generateId from '../utilites/IdentifierGenerator';
import { setGlobalState } from '../hooks/CurrentUser';
import logonShema from '../hooks/Validators/LogonUserValidator';

import React from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';

class SignUpForm extends React.Component {
  constructor(props) {
    super(props);
    this.close = props.closePopup;
  }

  // Trying to add info to storages.
  processUserInfo = info => {
    let identifier = generateId(20);
    if (!tryStoreToken(identifier, info.Token())){
      return false;
    }

    setGlobalState('name', info.Name());
    setGlobalState('surname', info.Surname());
    setGlobalState('patronymic', info.Patronymic());
    setGlobalState('birth_day', info.BirthDay());
    setGlobalState('city', info.City());
    setGlobalState('address', info.Address());
    setGlobalState('tokenStorageKey', identifier);
    return true;
  }

  // Sending new user info.
  tryLogon = async logonData => {
    let info = await logonUser(logonData.login, logonData.password);
    if (info === null){
      return false;
    }
    
    return this.processUserInfo(info);

  };

  // Handlind submissions.
  handleSubmittion = async event => {
    let logonData = {
      login: event.target[0].value,
      password: event.target[1].value
    }

    if (!await logonShema.isValid(logonData)){
      return;
    }

    if (await this.tryLogon(logonData)){
      this.close();
    }
    else{
      alert('Не удалось войти!');
    }
  };

  render() {
    return (
      <div className='popup'>
        <Button variant="success" value="Submit" className="popup-x" onClick={()=> this.close()} >X</Button>
        <Form className='popup_inner' onSubmit={this.handleSubmittion}>
          <Form.Group className="mb-3">
              <Form.Label>Логин:</Form.Label>
              <Form.Control type="text" />
            </Form.Group>
            <Form.Group>
              <Form.Label>Пароль:</Form.Label>
              <Form.Control type="password" />
          </Form.Group>
          <Col align="center"><Button as="a" variant="success" value="Submit">Войти</Button></Col>
        </Form>
      </div>
    );
  }
}

export default SignUpForm;