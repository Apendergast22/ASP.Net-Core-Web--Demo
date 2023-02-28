import './AuthentificationPanel.css'

import React from 'react';
import { useSelector } from 'react-redux';

import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';

function AuthentificatiomPanel() {
    const logon = useSelector(state => state.user.logon);
    const email = useSelector(state => state.user.email);
    if (logon) {
        return (
            <div>
                <ButtonGroup>
                    <Button href='/signin'>Авторизация</Button>
                    <Button href='/signup'>Регистрация</Button>
                </ButtonGroup>
            </div>
        )
    } 
    else {
        return(
            <Form.Group>
                <Form.Label as="legend" column sm={0}>{email}</Form.Label>
                <Button sm={2}>Выход</Button>)
            </Form.Group>
        )
    }
}

export default AuthentificatiomPanel;