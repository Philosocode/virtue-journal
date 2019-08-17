import React, { Component } from 'react'
import { AuthState } from '../redux/auth';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';

import { AppState } from '../redux/store';
import { loginUser } from "../redux/auth";
import { LoadingState } from '../redux/loading';

interface LoginPageProps {
  auth: AuthState,
  loading: LoadingState
  loginUser: Function
}

class _LoginPage extends Component<LoginPageProps> {
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

    const { error, username, password } = this.state;

    if (error) this.setState({ error });

    this.props.loginUser(username, password)
      .then((res: any) => {
        console.log(this.props.auth.isLoggedIn);
        console.log(this.props.auth.currentUser);
      })
      .catch((err: Error) => console.log("ERROR:", err));
  }

  render() {
    const { isLoggedIn } = this.props.auth;
    const { isLoading } = this.props.loading;

    return (
      <div>
        { isLoggedIn && <Redirect to="/virtues" /> }
        <h1>Login Page</h1>
        { isLoading && <h2>Loading...</h2>}
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