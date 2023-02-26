import '../assets/NavBar.css'
import React from "react";
import { Navbar, Nav } from 'react-bootstrap'
import AuthentificatiomPanel from "./AuthentificatiomPanel";

function AppHeader() {
  return (
    <Navbar>
      <Navbar.Collapse>
        <Nav className="ml-auto">
            <Nav.Link className="headerLinks" eventKey={1} href="/">На главную</Nav.Link>
            <Nav.Link className="headerLinks" eventKey={2} href="/drinkers">Провека в базе алкоголиков</Nav.Link>
            <Nav.Link className="headerLinks" eventKey={3} href="/criminals">Проверка в базе подозреваемых по уголовным делам</Nav.Link>
        </Nav>
          <AuthentificatiomPanel class = "authentication" />
      </Navbar.Collapse>
    </Navbar>
  );
}

export default AppHeader;
