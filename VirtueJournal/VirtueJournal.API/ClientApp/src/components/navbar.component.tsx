import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import { connect } from 'react-redux';

import { AppState } from "../redux/store";
import { AuthState, logoutUser } from "../redux/auth";

interface Props {
  auth: AuthState,
  logoutUser: typeof logoutUser
}

class _Navbar extends Component<Props> {
  handleLogout = (evt: React.SyntheticEvent) => {
    evt.preventDefault();
    this.props.logoutUser();
  };

  render() {
    const { isLoggedIn } = this.props.auth;

    const notLoggedInLinks = (
      <>
        <li className="c-navbar__list-item">
          <NavLink className="c-navbar__link" activeClassName="c-navbar__link--is-active"
            to="/login">Login</NavLink>
        </li>
        <li className="c-navbar__list-item">
          <NavLink className="c-navbar__link"  activeClassName="c-navbar__link--is-active"
            to="/register">Register</NavLink>
        </li>
      </>
    );

    const loggedInLinks = (
      <>
        <li className="c-navbar__list-item">
          <NavLink className="c-navbar__link" activeClassName="c-navbar__link--is-active"
            to="/virtues">Virtues</NavLink>
        </li>

        <li className="c-navbar__list-item">
          <a href="/logout" onClick={this.handleLogout} className="c-navbar__link">Logout</a>
        </li>
      </>
    );

    return (
      <header>
        <nav className="c-navbar">
          <ul className="c-navbar__list">
            <li className="c-navbar__list-item">
              <NavLink className="c-navbar__link" activeClassName="c-navbar__link--is-active"
                to="/" exact>Home</NavLink>
            </li>
            { isLoggedIn ? loggedInLinks : notLoggedInLinks }
          </ul>
        </nav>
      </header>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  auth: state.auth
});

export const Navbar = connect(
  mapStateToProps,
  { logoutUser }
)(_Navbar);
 