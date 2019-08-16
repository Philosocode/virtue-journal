export interface LoadingState {
  isLoading: boolean;
}

export enum LoadingConstants {
  LOADING_START = "LOADING_START",
  LOADING_STOP = "LOADING_STOP"
}

export interface LoadingStartAction {
  type: LoadingConstants.LOADING_START;
}

export interface LoadingStopAction {
  type: LoadingConstants.LOADING_STOP;
}

export type LoadingAction = LoadingStartAction | LoadingStopAction;