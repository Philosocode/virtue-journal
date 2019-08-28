import React from "react";
import { Route, Redirect, RouteProps } from "react-router-dom";
import { connect } from 'react-redux';

import { logoutUser, LogoutAction } from "../redux/auth";

interface Props extends RouteProps {
  component: any,
  isLoggedIn: boolean,
  logoutUser: () => LogoutAction
}

// Higher-order component that checks authentication status
// If authenticated, render the private component
// If not authenticated, redirect to /login
// See: https://tylermcginnis.com/react-router-protected-routes-authentication/
class _PrivateRoute extends React.Component<Props> {
  componentDidMount() {
    if (!this.props.isLoggedIn) {
      this.props.logoutUser();
    }
  }

  render() {
    const { component: Component, isLoggedIn, ...rest } = this.props;

    return (
      <Route
        {...rest}
        render={props => (
          isLoggedIn
            ? <Component {...props} />
            : <Redirect to="/login" />
        )}
      />
    );
  }
}

const mapStateToProps = (state: any) => ({
    isLoggedIn: state.auth.isLoggedIn
  });
  
export const PrivateRoute = connect(
mapStateToProps, 
{ logoutUser }
)(_PrivateRoute);  
