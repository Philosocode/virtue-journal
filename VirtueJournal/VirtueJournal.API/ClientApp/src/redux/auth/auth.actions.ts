import axios from "axios";
import { Dispatch } from "redux";

import {
  AuthConstants,
  LoginSuccessAction,
  LoginFailureAction,
  LogoutAction,
  UserForRegister
} from "./auth.types";

import { startLoading, stopLoading } from "../loading";

const ROOT_URL = "api/auth";

export const registerUser = async (user: UserForRegister) => {
  return axios.post(`${ROOT_URL}/register`, user);
}

export const loginUser = (username: string, password: string) => async (
  dispatch: Dispatch
) => {
  dispatch(startLoading());

  const loginData = { username, password };

  try {
    const userData = await axios.post(`${ROOT_URL}/authenticate`, loginData);

    // store user details and jwt token in local storage
    // to keep user logged in between page refreshes
    localStorage.setItem("user", JSON.stringify(userData));

    dispatch<LoginSuccessAction>({
      type: AuthConstants.LOGIN_SUCCESS,
      payload: userData
    });
  } catch (err) {
    dispatch<LoginFailureAction>({ type: AuthConstants.LOGIN_FAILURE });
    throw new Error(err);
  } finally {
    dispatch(stopLoading());
  }
};

export const logoutUser = (): LogoutAction => {
  localStorage.removeItem("user");

  return { type: AuthConstants.LOGOUT };
};
