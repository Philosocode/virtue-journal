import { LoadingConstants, LoadingStartAction, LoadingStopAction } from "./loading.types";

export const startLoading = (): LoadingStartAction => ({
  type: LoadingConstants.LOADING_START
});

export const stopLoading = (): LoadingStopAction => ({
  type: LoadingConstants.LOADING_STOP
});