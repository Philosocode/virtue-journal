import axios from "axios";
import { Dispatch } from "redux";

import {
  AuthTypes,
  LoginSuccessAction,
  LoginFailureAction,
  LogoutAction
} from "./auth.types";

import { startLoading, stopLoading } from "../loading";

export const loginUser = (username: string, password: string) => {
  return async (dispatch: Dispatch) => {
    startLoading();

    const loginData = { username, password };

    try {
      const userData = await axios.post(`api/auth/authenticate`, loginData);

      // store user details and jwt token in local storage
      // to keep user logged in between page refreshes
      localStorage.setItem("user", JSON.stringify(userData));

      dispatch<LoginSuccessAction>({
        type: AuthTypes.LOGIN_SUCCESS,
        payload: userData
      });
    } catch (err) {
      dispatch<LoginFailureAction>({
        type: AuthTypes.LOGIN_FAILURE,
        payload: err
      });
    } finally {
      stopLoading();
    }
  };
};

export const logoutUser = (): LogoutAction => {
  localStorage.removeItem("user");

  return { type: AuthTypes.LOGOUT };
};
