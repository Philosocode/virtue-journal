import React, { Component } from 'react'
import { Redirect } from 'react-router';
import { connect } from 'react-redux';
import axios, { AxiosResponse, AxiosError } from "axios";

import { AppState } from '../redux/store';
import { AuthState, registerUser } from "../redux/auth";

interface RegisterPageProps {
  auth: AuthState,
  registerUser: typeof registerUser
}

const initialState = {
  firstName: "",
  lastName: "",
  email: "",
  username: "",
  password: "",
  confirmPassword: "",
  shouldRedirectToLogin: false,
  errorMessage: ""
};

class _RegisterPage extends Component<RegisterPageProps> {
  state = {...initialState};

  componentDidMount() {
    if (this.props.auth.isLoggedIn) {
      this.setState({ shouldRedirectToLogin: true });
    }
  }

  componentWillUnmount() {
    this.setState(initialState);
  }

  handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };

  handleSubmit = async (event: React.SyntheticEvent) => {
    event.preventDefault();

    if (!this.validateForm()) return;

    const { firstName, lastName, email, username, password } = this.state;
    const userForRegister = { firstName, lastName, email, username, password };

    axios.post("api/auth/register", userForRegister)
      .then((res: AxiosResponse) => {
        this.setState({ shouldRedirectToLogin: true });
      })
      .catch((err: AxiosError) => {
        const errorResponse = err.response;

        if (errorResponse) {
          this.setState({ errorMessage: `Error: ${errorResponse.data}` });
        }
      });
  }

  validateForm = () => {
    const { password, confirmPassword } = this.state

    if (password !== confirmPassword) return false;

    return true;
  }

  render() {
    const { shouldRedirectToLogin } = this.state;
    const { isLoggedIn } = this.props.auth;

    return (
      <div>
        { isLoggedIn && <Redirect to="/virtues" /> }
        { shouldRedirectToLogin && <Redirect to="/login" /> }
        <h1>Register Page</h1>
        <form className="form" onSubmit={this.handleSubmit}>
          <div className="form__group">
            <label htmlFor="firstName" className="form__label">First Name:</label>
            <div className="form__input-container">
              <input
                required
                id="firstName"
                className="form__input"
                type="text"
                name="firstName"
                placeholder="First Name"
                value={this.state.firstName}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="lastName" className="form__label">Last Name:</label>
            <div className="form__input-container">
              <input
                required
                id="lastName"
                className="form__input"
                type="text"
                name="lastName"
                placeholder="Last Name"
                value={this.state.lastName}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="email" className="form__label">Email</label>
            <div className="form__input-container">
              <input
                required
                id="email"
                className="form__input"
                type="email"
                name="email"
                placeholder="Email Address"
                value={this.state.email}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="username" className="form__label">Username:</label>
            <div className="form__input-container">
              <input
                required
                id="username"
                className="form__input"
                type="text"
                name="username"
                placeholder="Username"
                value={this.state.username}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="password" className="form__label">Password:</label>
            <div className="form__input-container password">
              <input
                required
                id="password"
                className="form__input"
                type="password"
                name="password"
                placeholder="Password"
                value={this.state.password}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="confirmPassword" className="form__label">Re-enter password: </label>
            <div className="form__input-container">
              <input
                required
                id="confirmPassword"
                className="form__input"
                type="password"
                name="confirmPassword"
                placeholder="Re-enter password"
                value={this.state.confirmPassword}
                onChange={this.handleChange}
              />
              {/* <div className="error">{this.state.confirmPassword}</div> */}
            </div>
          </div>


          <button className="button" type="submit">Register</button>
          <div className="error error--server">{this.state.errorMessage}</div>
          {/* { isLoading && <p>Loading...</p>} */}
        </form>
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  auth: state.auth
});

export const RegisterPage = connect(
  mapStateToProps,
  { registerUser }
)(_RegisterPage);