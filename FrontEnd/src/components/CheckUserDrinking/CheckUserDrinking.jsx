import './CheckUserDrinking.css'

import * as React from 'react';
import { useFormik } from "formik";

import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Container from 'react-bootstrap/Container';
import Col from 'react-bootstrap/Col';

// import DrinkerAnimation from './DrinkerAnimation'
import { CheckLogonUser, CheckUser } from '../../services/api/CheckDrinker'
import checkDrinkerShema from './ValidationSchema'

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
    const refDrinkerAnimation = React.useRef();
    const HandleSubmitMain = async (values, actions) => { 
    try{
        let checkingResult = IsUserLogon()
            ? await CheckLogonUser()
            : await CheckUser(values.name, values.surname, values.patronymic, values.birth_day);

        refDrinkerAnimation.StartAnimation(checkingResult["doesUserDrinker"]);
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
      onSubmit: async (values, actions) => await HandleSubmitMain(values, actions),
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
            <Col align="right"><Button variant="success" type="submit" disabled={isSubmitting}>Отправить данные</Button></Col>
           </Container>
        </Form>
   </div>);
}

export default CheckUserDrinking;
