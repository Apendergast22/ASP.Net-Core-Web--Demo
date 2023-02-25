import '../assets/drink/DrinkForm.css';
import * as React from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Stack from 'react-bootstrap/Stack';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import DrinkerAnimation from './DrinkerAnimation'
import { checkLogonUser, checkUser } from '../hooks/FetchRequest/CheckDrinker'
import { useUserState } from '../hooks/CurrentUser'
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

// https://react-bootstrap.github.io/layout/stack/
function DrinkForm(){

  const [disableForms, setDisableForm] = React.useState(false);
  const refDrinkerAnimation = React.useRef();  
  const [ tokenKey, ] = useUserState('tokenStorageKey');
  setDisableForm(tokenKey !== null);

  const [ name, ] = useUserState('name');
  const [ surname, ] = useUserState('surname');
  const [ patronymic, ] = useUserState('patronymic');
  const [ birth_day, ] = useUserState('birth_day');
  
  const HandleSubmit = async event => {
    const [ tokenKey, ] = useUserState('tokenStorageKey');
    if ((tokenKey === null) && !(await checkUserData(event))){
      return;
    }
      
    try{
      let checkingResult = tokenKey == null
        ? await checkLogonUser()
        : await checkUser();

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
          <Form onSubmit={HandleSubmit}>
            <Form.Group as={Row} className="mb-3" controlId="surname">
              <Form.Label as="legend" column sm={0}>Фамилия: </Form.Label>
              <Col sm={8}><Form.Control type="text" placeholder="Введите вашу фамилию..." value={disableForms ? surname : ""} disabled={disableForms} /></Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3" controlId="name">
              <Form.Label as="legend" column sm={0}>Имя: </Form.Label>
              <Col sm={8}><Form.Control type="text" placeholder="Введите ваше имя..." value={disableForms ? name : ""} disabled={disableForms} /></Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3" controlId="patronymic">
              <Form.Label as="legend" column sm={0}>Очество: </Form.Label>
              <Col sm={8}><Form.Control type="text" placeholder="Введите ваше очество..." value={disableForms ? patronymic : ""} disabled={disableForms} /></Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3" controlId="birthday">
              <Form.Label as="legend" column sm={0}>Год рождения: </Form.Label>
              <Col sm={8}><Form.Control type="date" value={disableForms ? birth_day : ""} disabled={disableForms} /></Col>
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