import '../assets/drink/DrinkForm.css';
import * as React from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Stack from 'react-bootstrap/Stack';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import DrinkerAnimation from './DrinkerAnimation'
import { CheckLogonUser, CheckUser } from '../hooks/FetchRequest/CheckDrinker'
import { useGlobalState } from '../hooks/CurrentUser'
import checkDrinkerShema from '../hooks/Validators/CheckDrinkerValidator'

function parseErrorMessage(errorJson) {
  let targetProperty = "";
  if (errorJson["instance"] == "Name"){
    targetProperty = "Имя";
  } else if (errorJson["instance"] == "Surname"){
    targetProperty = "Фамилия";
  }

  if (errorJson["detail"] == ""){
    return "Поле \'" + targetProperty + "\' не может быть пустым!";
  }
  else{
    return "\'" + targetProperty + "\' имеет некорректное значение!";
  }
}

  // Cheking data that tasted user.
const checkUserData = async eventWithData => {
  let userData = {
    surname: eventWithData.target[0].value,
    name: eventWithData.target[1].value,
    patronymic: eventWithData.target[2].value,
    birth_day: eventWithData.target[3].value
  }

  return await checkDrinkerShema.isValid(userData);
}

function IsUserLogon() {
  const [ tokenKey, ] = useGlobalState("tokenStorageKey");
  return tokenKey !== null;
}

// https://react-bootstrap.github.io/layout/stack/
function DrinkForm(){
  const refDrinkerAnimation = React.useRef();
  const [private_info, ] = useGlobalState('private');
  const HandleSubmit = async event => {
  const [tokenKey, ] = useGlobalState('tokenStorageKey');
  if ((tokenKey === null) && !(await checkUserData(event))){
    return;
  }
      
  try{
    let checkingResult = tokenKey == null
      ? await CheckLogonUser()
      : await CheckUser();

    refDrinkerAnimation.StartAnimation(checkingResult["doesUserDrinker"]);
  }
  catch (exception){
    alert(parseErrorMessage(exception.Details()));
  }
}

  return (
   <div className="DrinkForm">
     <Stack direction="horizontal" gap={3}>
       <Col sm={5}>
          <Form onSubmit={HandleSubmit.bind(this)}>
            <Form.Group as={Row} className="mb-3" controlId="surname">
              <Form.Label as="legend" column sm={0}>Фамилия: </Form.Label>
              <Col sm={8}><Form.Control type="text" placeholder="Введите вашу фамилию..." defaultValue={IsUserLogon() ? private_info.surname : ""} disabled={IsUserLogon()} /></Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3" controlId="name">
              <Form.Label as="legend" column sm={0}>Имя: </Form.Label>
              <Col sm={8}><Form.Control type="text" placeholder="Введите ваше имя..." defaultValue={IsUserLogon() ? private_info.name : ""} disabled={IsUserLogon()} /></Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3" controlId="patronymic">
              <Form.Label as="legend" column sm={0}>Очество: </Form.Label>
              <Col sm={8}><Form.Control type="text" placeholder="Введите ваше очество..." defaultValue={IsUserLogon() ? private_info.patronymic : ""} disabled={IsUserLogon()} /></Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3" controlId="birthday">
              <Form.Label as="legend" column sm={0}>Год рождения: </Form.Label>
              <Col sm={8}><Form.Control type="date" defaultValue={IsUserLogon() ? private_info.birth_day : ""} disabled={IsUserLogon()} /></Col>
            </Form.Group>
            <Col align="right"><Button as="a" variant="success" value="Submit" >Отправить данные</Button></Col>
         </Form>
         </Col>
        <Col sm={5}>
          <DrinkerAnimation ref={refDrinkerAnimation} />
        </Col>
      </Stack>
    </div>
  );
}

export default DrinkForm;