import RegistryNewUserForm from './RegistrationUserForm'
import SignUpForm from './LogonForm'
import { useUserState, clearInfo } from '../hooks/CurrentUser';
import { removeToken } from '../utilites/StorageService'

import React from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';

function AuthentificatiomPanel() {
    const [state, setState ] = React.useState({ registration: false, signUp: false });

    // Handling logout
    const HandleLogoutClick = () => {
        const [ tokenKey, ] = useUserState('tokenStorageKey');
        clearInfo();
        removeToken(tokenKey);
    }

    const ToggleRegistration = () => {
        setState({ registration: !state.registration });
      }
    
    const ToggleSignUp = () => {
        setState({ signUp: !state.signUp });
    }
 
    const [ tokenKey, ] = useUserState('tokenStorageKey');
    const [ name, ] = useUserState('name');
    const [ patronymic, ] = useUserState('patronymic');  
    if (tokenKey === null) {
        return (
            <div>
                <ButtonGroup>
                    <Button variant="secondary" onClick = {ToggleSignUp.bind(this)}>Авторизация</Button>
                    <Button variant="secondary"  onClick={ToggleRegistration.bind(this)}>Регистрация</Button>
                </ButtonGroup>
                {
                    state.registration 
                        ? <RegistryNewUserForm closePopup = {ToggleRegistration.bind(this)} />
                        : null
                }
                {
                    state.signUp 
                    ? <SignUpForm closePopup = {ToggleSignUp.bind(this)} />
                    : null
                }
            </div>
        )
    } 
    else {  
        return(
            <Form.Group>
                <Form.Label as="legend" column sm={0}>{name} {patronymic}</Form.Label>
                <Button sm={2} variant="secondary" onClick={HandleLogoutClick}>Выход</Button>)
            </Form.Group>
        )
    }
}

export default AuthentificatiomPanel;