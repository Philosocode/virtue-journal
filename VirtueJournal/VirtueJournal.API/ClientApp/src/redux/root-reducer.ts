import { combineReducers } from "redux";

import { virtueReducer } from "./virtue";
import { entryReducer } from "./entry";
import { authReducer } from "./auth";
import { errorReducer } from "./error";
import { loadingReducer } from "./loading";

export const rootReducer = combineReducers({
  auth: authReducer,
  entry: entryReducer,
  error: errorReducer,
  loading: loadingReducer,
  virtue: virtueReducer,
});