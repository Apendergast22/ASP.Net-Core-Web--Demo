import './NavBar.css'
import React from "react";
import { Navbar, Nav } from 'react-bootstrap'
import AuthentificatiomPanel from "../Authentification/AuthentificationPanel";

function AppHeader() {
  return (
    <Navbar>
        <Navbar.Collapse>
            <Nav>
                <Nav.Link className='nav-link' href="/">На главную</Nav.Link>
                <Nav.Link className='nav-link' href="/drinkers">Алкоголики</Nav.Link>
                <Nav.Link className='nav-link' href="/criminals">Уголовники</Nav.Link>
            </Nav>
        </Navbar.Collapse>
        <AuthentificatiomPanel/>
    </Navbar>
  );
}

export default AppHeader;
