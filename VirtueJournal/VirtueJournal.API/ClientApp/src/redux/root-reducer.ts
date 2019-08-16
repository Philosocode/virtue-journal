import { combineReducers } from "redux";

// import { Virtue, virtueReducer } from "./virtue";
// import { Entry, entryReducer } from "./entry";
import { loadingReducer, LoadingState } from "./loading";
import { authReducer, AuthState } from "./auth";

export interface StoreState {
  auth: AuthState,
  // virtues: Virtue[],
  // entries: Entry[],
  loading: LoadingState
}

export const rootReducer = combineReducers<StoreState>({
  auth: authReducer,
  // entries: entryReducer,
  // errors: errorReducer,
  loading: loadingReducer,
  // virtues: virtueReducer,
});