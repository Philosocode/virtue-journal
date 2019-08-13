import React, { Component } from "react";
import { NavLink } from "react-router-dom";

class Navbar extends Component {
  render() {
    return (
      <header>
        <nav className="c-navbar">
          <ul className="c-navbar__list">
            <li className="c-navbar__list-item">
              <NavLink className="c-navbar__link" activeClassName="c-navbar__link--is-active"
                to="/" exact>Home</NavLink>
            </li>
            <li className="c-navbar__list-item">
              <NavLink className="c-navbar__link" activeClassName="c-navbar__link--is-active"
                to="/virtues">Virtues</NavLink>
            </li>
            <li className="c-navbar__list-item">
              <NavLink className="c-navbar__link" activeClassName="c-navbar__link--is-active"
                to="/login">Login</NavLink>
            </li>
            <li className="c-navbar__list-item">
              <NavLink className="c-navbar__link"  activeClassName="c-navbar__link--is-active"
                to="/register">Register</NavLink>
            </li>
          </ul>
        </nav>
      </header>
    )
  }
}

export { Navbar };