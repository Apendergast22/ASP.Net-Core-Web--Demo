import './SignUp.css'

import React from 'react';
import { useFormik } from "formik";

import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import FloatingLabel  from 'react-bootstrap/FloatingLabel';

import { OpenPreviousPage } from '../../utilites/CommonUtils'
import signUpShema from './ValidationSchema'
import signUp from '../../services/api/SignUp'

const SignUp = () => {
    const SignUp = async (values, actions) => {
        try{
            await signUp(values.email, values.password)
            OpenPreviousPage();
        }
        catch (exception){
          alert("Неизвестная ошибка: " + exception.Details());
        }
        finally{
          actions.resetForm();
        }
    }

    const { values, errors, touched, isSubmitting, handleBlur, handleChange, handleSubmit, } = useFormik(
        {
        initialValues: {
            email: "",
            password: "",
            name: "",
            patronymic: "",
            surname: "",
            birthDay: "1990-01-01",
            city: "",
            street: "",
            build: "",
            apartment: ""
        },
        validationSchema: signUpShema,
        onSubmit: async (values, actions) => await SignUp(values, actions),
        });

    return(
    <div>
        <Form>
            <Container className="mb-3">
                <Form.Group className="mb-3">
                    <Form.Label>Регистрация нового пользователя</Form.Label>
                    <Col md>
                        <FloatingLabel label="e-mail" controlId="email">
                            <Form.Control
                                type="email"
                                value={values.email}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className={errors.email && touched.email ? "input-error" : "form-control"} />
                            <Form.Text>{errors.email}</Form.Text>
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Пароль" controlId="password">
                            <Form.Control
                                type="password"
                                value={values.password}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className={errors.password && touched.password ? "input-error" : "form-control"} />
                            <Form.Text>{errors.password}</Form.Text>
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Имя" controlId="name">
                            <Form.Control
                                type="text"
                                value={values.name}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className={errors.name && touched.name ? "input-error" : "form-control"} />
                            <Form.Text>{errors.name}</Form.Text>
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Отчество" controlId="patronymic">
                            <Form.Control
                                type="text"
                                value={values.patronymic}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className={errors.patronymic && touched.patronymic ? "input-error" : "form-control"} />
                            <Form.Text>{errors.patronymic}</Form.Text>
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Фамилия" controlId="surname">
                            <Form.Control
                                type="text"
                                value={values.surname}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className={errors.surname && touched.surname ? "input-error" : "form-control"} />
                            <Form.Text>{errors.surname}</Form.Text>
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Дата рождения" controlId="birthDay">
                            <Form.Control
                                type="date"
                                value={values.birthDay}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className={errors.birthDay && touched.birthDay ? "input-error" : "form-control"} />
                            <Form.Text>{errors.birthDay}</Form.Text>
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Город рождения" controlId="city">
                            <Form.Control
                                type="text"
                                value={values.city}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className={errors.city && touched.city ? "input-error" : "form-control"} />
                            <Form.Text>{errors.city}</Form.Text>
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Улица" controlId="street">
                            <Form.Control
                                type="text"
                                value={values.street}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className={errors.street && touched.street ? "input-error" : "form-control"} />
                            <Form.Text>{errors.street}</Form.Text>
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Номер дома, корпус/строение" controlId="build">
                            <Form.Control
                                type="text"
                                value={values.build}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className='form-control' />
                        </FloatingLabel>
                    </Col>
                    <Col md>
                        <FloatingLabel label="Номер квартиры" controlId="apartment">
                            <Form.Control
                                type="text"
                                value={values.apartment}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                className='form-control' />
                        </FloatingLabel>
                    </Col>
                </Form.Group>
                <Col align="center"><Button as="a" variant="success" value="Submit" >Регистрация</Button></Col>
            </Container>
        </Form>
    </div>);
}

export default SignUp;