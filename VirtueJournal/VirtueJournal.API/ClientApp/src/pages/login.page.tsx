import React, { Component } from 'react'
import { AuthState } from '../redux/auth';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';

import { AppState } from '../redux/store';
import { loginUser } from "../redux/auth";
import { LoadingState } from '../redux/loading';

interface Props {
  auth: AuthState,
  loading: LoadingState
  loginUser: Function
}

class _LoginPage extends Component<Props> {
  state = {
    username: "",
    password: "",
    error: ""
  }

  handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };

  handleSubmit = async (event: React.SyntheticEvent) => {
    event.preventDefault();

    const { username, password } = this.state;

    this.props.loginUser(username, password)
      .catch((err: Error) => this.setState({ error: err.message }));
  }

  render() {
    const { isLoggedIn } = this.props.auth;
    const { isLoading } = this.props.loading;

    return (
      <div>
        { isLoggedIn && <Redirect to="/virtues" /> }
        <h1>Login Page</h1>
        <form className="form" onSubmit={this.handleSubmit}>

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

          <button className="button" type="submit">Login</button>
          <div className="error error--server">{this.state.error}</div>
          { isLoading && <p>Loading...</p>}
        </form>
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  auth: state.auth,
  loading: state.loading
});

export const LoginPage = connect(
  mapStateToProps,
  { loginUser }
)(_LoginPage);