import { AuthConstants, AuthAction, AuthState, User } from "./auth.types";

let user;
const userJson = localStorage.getItem("user");
if (userJson) {
  user = JSON.parse(userJson) as User;
}

const initialState: AuthState = user
  ? { currentUser: user, isLoggedIn: true }
  : { isLoggedIn: false };

export const authReducer = (state = initialState, action: AuthAction) => {
  switch (action.type) {
    case AuthConstants.LOGIN_SUCCESS:
      return {
        isLoggedIn: true,
        user: action.payload
      };
    case AuthConstants.LOGIN_FAILURE:
      return {
        isLoggedIn: false,
        user: null
      };
    case AuthConstants.LOGOUT:
      return {
        isLoggedIn: false,
        user: null
      };
    default:
      return state;
  }
};
