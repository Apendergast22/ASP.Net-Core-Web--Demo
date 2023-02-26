import '../assets/UserForm.css';
import registrationShema from '../hooks/Validators/RegistrationUserValidator';
import registryNewUser from '../hooks/FetchRequest/RegistrationUser';
import generateId from '../utilites/IdentifierGenerator';
import { buildAddress } from '../utilites/CommonUtils';
import { useGlobalState} from '../hooks/CurrentUser';
import { tryStoreToken } from '../utilites/StorageService'

import React from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import { FloatingLabel } from 'react-bootstrap';

class RegistryNewUserForm extends React.Component {
  constructor(props) {
    super(props);
    this.close = props.closePopup;
  }

  // Sending new user info.
  fetchRegistryUserInfo = async (registrationData, build, appartament) => {
    const [ , setPrivateInfo] = useGlobalState('private');

    setPrivateInfo({
      name: registrationData.name,
      surname: registrationData.surname,
      patronymic: registrationData.patronymic,
      birth_day: registrationData.birth_day,
      city: registrationData.city,
      address: buildAddress(registrationData.street, build, appartament)
     });
     
    return await registryNewUser(registrationData.login, registrationData.password);
  };

  // Token processing.
  processToken = token => {
    const [ , setTokenStorageKey] = useGlobalState('tokenStorageKey');
    let identifier = generateId(20)
    tryStoreToken(identifier, token);
    setTokenStorageKey(identifier);
  }
  
  // Handling submissions.
  handleSubmittion = async event => {
    let registrationData = {
      login: event.target[0].value,
      password: event.target[1].value,
      name: event.target[2].value,
      surname: event.target[3].value,
      patronymic: event.target[4].value,
      birth_day: event.target[5].value,
      city: event.target[6].value,
      street: event.target[7].value
    }

    if (!await registrationShema.isValid(registrationData)){
      return;
    }
    
    let token = await this.fetchRegistryUserInfo(registrationData, event.target[7].value, event.target[8].value);
    if (token === null){
      alert("Не удалось создать нового пользователя!");
    }
    else{
      this.processToken(token);
      this.close();
    }
  };

  render() {
    return (
        <div className='popup'>
          <Button variant="success" value="Submit" className="popup-x" onClick={()=> this.close()} >X</Button>
          <Form className='popup_inner' onSubmit={this.handleSubmittion.bind(this)}>
            <Form.Group className="mb-3">
              <Form.Label>Введите сведения:</Form.Label>
              <Col md><FloatingLabel label="Логин"><Form.Control type="text" /></FloatingLabel></Col>
              <Col md><FloatingLabel label="Пароль"><Form.Control type="password" /></FloatingLabel></Col>
            </Form.Group>
            <Form.Group className="mb-3">
              <Col md><FloatingLabel label="Имя"><Form.Control type="text" /></FloatingLabel></Col>
              <Col md><FloatingLabel label="Отчество"><Form.Control type="text" /></FloatingLabel></Col>
            </Form.Group>
            <Form.Group className="mb-3">
              <Col md><FloatingLabel label="Фамилия"><Form.Control type="text" /></FloatingLabel></Col>
              <Col md><FloatingLabel label="Дата рождения"><Form.Control type="date" /></FloatingLabel></Col>
            </Form.Group>
            <Form.Group className="mb-3">
              <Col md><FloatingLabel label="Город рождения"><Form.Control type="text" /></FloatingLabel></Col>
              <Col md><FloatingLabel label="Улица"><Form.Control type="text" /></FloatingLabel></Col>
              <Col md><FloatingLabel label="Номер дома, корпус/строение"><Form.Control type="text" /></FloatingLabel></Col>
              <Col md><FloatingLabel label="Номер квартиры"><Form.Control type="text" /></FloatingLabel></Col>
            </Form.Group>
            <Col align="center"><Button as="a" variant="success" value="Submit" >Регистрация</Button></Col>
          </Form>
        </div>
      );
    }
  }

export default RegistryNewUserForm;
