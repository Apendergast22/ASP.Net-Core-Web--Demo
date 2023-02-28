import './NavBar.css'
import 'react-tooltip/dist/react-tooltip.css'

import React from "react";
import { Tooltip } from 'react-tooltip'

import { useDispatch } from "react-redux";
import { useSelector } from 'react-redux';
import { Navbar, Nav } from 'react-bootstrap'

import AuthentificatiomPanel from "../Authentification/AuthentificationPanel";
import { onNavigate } from '../../services/redux/pathSlice';
import { INDEX, DRINK, CRIMINALS } from '../../utilites/Paths'

// Navigation bar component.
function NavigationBar() {
  const dispath = useDispatch();
  const logon = useSelector(state => state.user.logon);
  return (
    <Navbar>
        <Navbar.Collapse>
            <Nav>
                <Nav.Link className='nav-link' onSelect={() => dispath(onNavigate(INDEX))} href={INDEX}>На главную</Nav.Link>
                <Nav.Link className='nav-link' onSelect={() => dispath(onNavigate(DRINK))} href={DRINK}>Алкоголики</Nav.Link>
                <Nav.Link className='nav-link' onSelect={() => dispath(onNavigate(CRIMINALS))} href={CRIMINALS} data-tooltip-id="not-avaliable">Уголовники</Nav.Link>
                <Tooltip id="not-avaliable" place="top" effect="solid">{!logon ? "Только авторизованные пользователи" : ""}</Tooltip>
            </Nav>
        </Navbar.Collapse>
        <AuthentificatiomPanel/>
    </Navbar>
  );
}

export default NavigationBar;
