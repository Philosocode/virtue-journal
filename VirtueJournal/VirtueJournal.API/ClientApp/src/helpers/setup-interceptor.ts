import axios from "axios";

import { store } from "../redux/store";
import { logoutUser } from "../redux/auth";

const UNAUTHORIZED = 401;

axios.interceptors.response.use(response => {
  console.log("Interceptor");
  
  return response;
}, error => {
  console.log("Interceptor called.");

  const { status } = error.response;

  if (status === UNAUTHORIZED) {
    store.dispatch(logoutUser());
  }

  // return Promise.reject(error);
 }
);