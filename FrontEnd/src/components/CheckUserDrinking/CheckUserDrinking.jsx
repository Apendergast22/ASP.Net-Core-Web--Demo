import './CheckUserDrinking.css';
import vodka_image from'../../assets/drink/Vodka.png';
import juice_image from'../../assets/drink/Juice.png';

import * as React from 'react';
import { useFormik } from "formik";

import { View, Image, Text } from 'react-native';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Container from 'react-bootstrap/Container';
import Col from 'react-bootstrap/Col';
import ListGroup from 'react-bootstrap/ListGroup'
import { sleep_ms } from '../../utilites/CommonUtils';

import Modal from '../CommonModal/Modal';
import { CheckLogonUser, CheckUser } from '../../services/api/CheckDrinker';
import checkDrinkerShema from './ValidationSchema';

function parseErrorMessage(errorJson) {
  let targetProperty = "";
  if (errorJson["instance"] == "Name"){
    targetProperty = "Имя";
  } else if (errorJson["instance"] == "Surname"){
    targetProperty = "Фамилия";
  }

  if (targetProperty === ""){
    return "Неизвестная ошибка: " + errorJson["fullError"];
  }

  if (errorJson["detail"] == ""){
    return "Поле \'" + targetProperty + "\' не может быть пустым!";
  }
  else{
    return "\'" + targetProperty + "\' имеет некорректное значение!";
  }
}

function IsUserLogon() {
  return false;
}

const CheckUserDrinking = () => {
  const [state, setState] = React.useState({ showVodka: false, showJuice: false });
  const [active, setActive] = React.useState(false);
  const StartAnimation = (isDrinker) => {
    setActive(true);
    const step = 100;
    for (let time = 0; time < 5000; time += step){
        setState({ showVodka : time % (2 * step) == 0 })
        setState({ showJuice : time % (2 * step) != 0 })
        sleep_ms(step);
    }
            
    setState({ showVodka : isDrinker })
    setState({ showJuice : !isDrinker })
    sleep_ms(5000);
    setState({ showVodka : false })
    setState({ showJuice : false })
    setActive(false);
  }
  const HandleSubmitMain = async (values, actions) => { 
    try{
      StartAnimation(true);
      let checkingResult = IsUserLogon()
        ? await CheckLogonUser()
        : await CheckUser(values.name, values.surname, values.patronymic, values.birth_day);

        StartAnimation(checkingResult["doesUserDrinker"]);
    }
    catch (exception){
      alert(exception)
      alert(parseErrorMessage(exception.Details()));
    }
    finally{
      actions.resetForm();
    }
  }

  const { values, errors, touched, isSubmitting, handleBlur, handleChange, handleSubmit, } = useFormik(
    {
      initialValues: {
        surname: IsUserLogon() ? "surname" : "",
        name: IsUserLogon() ? "name" : "",
        patronymic: IsUserLogon() ? "patronymic" : "",
        birthday: IsUserLogon() ? "2023-02-27" : "1990-01-01",
      },
      validationSchema: checkDrinkerShema,
      onSubmit: async (values, actions) => await HandleSubmitMain(values, actions)
    });

    return(<div>
        <Form onSubmit={values => handleSubmit(values)} autoComplete="off">
            <Container className="mb-3">
                <Form.Group className="mb-3" controlId="surname">
                    <Form.Label>Фамилия: </Form.Label>
                    <Form.Control
                        type="text"
                        value={values.surname}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        className={errors.surname && touched.surname ? "input-error" : "form-control"}
                        disabled={IsUserLogon()} />
                    <Form.Text>{errors.surname}</Form.Text>
            </Form.Group>
            <Form.Group controlId="name">
                <Form.Label>Имя: </Form.Label>
                <Form.Control
                    type="text"
                    value={values.name}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    className={errors.name && touched.name ? "input-error" : "form-control"}
                    disabled={IsUserLogon()} />
                <Form.Text>{errors.name}</Form.Text>
            </Form.Group>
            <Form.Group controlId="patronymic">
                <Form.Label>Очество: </Form.Label>
                <Form.Control
                    type="text"
                    value={values.patronymic}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    className={errors.patronymic && touched.patronymic ? "input-error" : "form-control"}
                    disabled={IsUserLogon()} />
                <Form.Text>{errors.patronymic}</Form.Text>
            </Form.Group>
            <Form.Group controlId="birthday">
                <Form.Label>Год рождения: </Form.Label>
                <Form.Control
                    type="date"
                    value={values.birthday}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    className={errors.birthday && touched.birthday ? "input-error" : "form-control"}
                    disabled={IsUserLogon()} />
                <Form.Text>{errors.birthday}</Form.Text>
            </Form.Group>
            <Col align="right"><Button variant="success" type="submit" disabled={isSubmitting} >Отправить данные</Button></Col>
           </Container>
        </Form>
        <Modal active={active} setActive={setActive}>
          <View>
            <ListGroup variant="flush" style={{ display: state.showVodka ? "flex" : "none" }}>
              <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Image source={vodka_image} style={{ width:96, height:256 }} ></Image></ListGroup.Item>
              <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Text>Водка - ваш выбор!!!</Text></ListGroup.Item>
            </ListGroup>
              <ListGroup variant="flush" style={{ display: state.showJuice ? "flex" : "none" }}>
                <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Image source={juice_image} style={{ width:96, height:256 }}></Image></ListGroup.Item>
                <ListGroup.Item style={{ resizeMode: 'contain', alignSelf: 'center'}}><Text>По всей видимости, вы не часто пьете...</Text></ListGroup.Item>
            </ListGroup>
          </View>
        </Modal>
   </div>);
}

export default CheckUserDrinking;
