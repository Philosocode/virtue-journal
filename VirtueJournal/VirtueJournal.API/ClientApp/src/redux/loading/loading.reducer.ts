import { LoadingState, LoadingConstants, LoadingAction } from "./loading.types";

const initialState: LoadingState = {
  isLoading: false
};

export const loadingReducer = function(
  state = initialState,
  action: LoadingAction
): LoadingState {
  switch (action.type) {
    case LoadingConstants.LOADING_START:
      return {
        isLoading: true
      };
    case LoadingConstants.LOADING_STOP:
      return {
        isLoading: false
      };
    default:
      return state;
  }
};
