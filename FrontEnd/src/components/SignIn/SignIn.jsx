import './SignIn.css'

import React from 'react';
import { useFormik } from "formik";

import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';

import signInShema from './ValidationSchema';
import { OpenPreviousPage } from '../../utilites/CommonUtils'
import signIn from '../../services/api/SignIn';

const SignIn = () => {
    const SignIn = async (values, actions) => {
        try{
            await signIn(values.email, values.password, values.keepUser)
            OpenPreviousPage();
        }
        catch (exception){
          alert("Неизвестная ошибка: " + exception.Details());
        }
        finally{
          actions.resetForm();
        }
    }

    const { values, errors, touched, isSubmitting, handleBlur, handleChange, handleSubmit } = useFormik(
        {
        initialValues: {
            email: "",
            password: "",
            keepUser: false
        },
        validationSchema: signInShema,
        onSubmit: async (values, actions) => await SignIn(values, actions),
        });

    return(
        <div>
            <Form onSubmit={values => handleSubmit(values)}>
                <Container>
                    <Form.Group controlId="email">
                        <Form.Label>e-mail:</Form.Label>
                        <Form.Control 
                            type="email"
                            value={values.email}
                            onChange={handleChange}
                            onBlur={handleBlur}
                            className={errors.email && touched.email ? "input-error" : "form-control"} />
                        <Form.Text>{errors.email}</Form.Text>
                    </Form.Group>
                    <Form.Group controlId="password">
                        <Form.Label>Пароль:</Form.Label>
                        <Form.Control type="password"
                            value={values.password}
                            onChange={handleChange}
                            onBlur={handleBlur}
                            className={errors.password && touched.password ? "input-error" : "form-control"} />
                        <Form.Text>{errors.password}</Form.Text>
                    </Form.Group>
                    <Form.Group controlId="keepUser">
                        <div className='control-panel'>
                            <Form.Check
                                type="switch"
                                value={values.keepUser}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                label="Оставаться в системе" />
                            <Button as="a" variant="success" value="Submit" disabled={isSubmitting}>Войти</Button>
                        </div>
                    </Form.Group>
                </Container>
            </Form>
        </div>);
}

export default SignIn;