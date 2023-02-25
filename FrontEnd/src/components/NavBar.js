import React from "react";
import { NavLink } from "react-router-dom";
import AuthentificatiomPanel from "./AuthentificatiomPanel"
import Form from 'react-bootstrap/Form'
import NavBar from 'react-bootstrap/NavBar'

function Header() {
  return (
    <NavBar>
        <NavBar.Collapse>
            <NavLink exact activeClassName="active" to="/">На главную</NavLink>
            <NavLink activeClassName="active" to="/drinkers">Провека в базе алкоголиков.</NavLink>
            <NavLink activeClassName="active" to="/criminals">Проверка в базе подозреваемых по уголовным делам.</NavLink>
        </NavBar.Collapse>
        <Form inline className="mx-3">
            <AuthentificatiomPanel />
        </Form>
    </NavBar>
  );
}

export default Header;