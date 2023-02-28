import './NavBar.css'

import React from "react";
import { useDispatch } from "react-redux";
import { Navbar, Nav } from 'react-bootstrap'

import AuthentificatiomPanel from "../Authentification/AuthentificationPanel";
import { onNavigate } from '../../services/redux/pathSlice';
import { INDEX, DRINK, CRIMINALS } from '../../utilites/Paths'

// Navigation bar component.
function NavigationBar() {
  const dispath = useDispatch();
  return (
    <Navbar>
        <Navbar.Collapse>
            <Nav>
                <Nav.Link className='nav-link' onSelect={dispath(onNavigate(INDEX))} href={INDEX}>На главную</Nav.Link>
                <Nav.Link className='nav-link' onSelect={dispath(onNavigate(DRINK))} href={DRINK}>Алкоголики</Nav.Link>
                <Nav.Link className='nav-link' onSelect={dispath(onNavigate(CRIMINALS))} href={CRIMINALS}>Уголовники</Nav.Link>
            </Nav>
        </Navbar.Collapse>
        <AuthentificatiomPanel/>
    </Navbar>
  );
}

export default NavigationBar;
