import { VirtueState, VirtueConstants, VirtueAction } from "./virtue.types";

const initialState: VirtueState = {
  currentVirtue: undefined,
  virtues: []
};

export const virtueReducer = (state = initialState, action: VirtueAction) => {
  switch (action.type) {
    case VirtueConstants.GET_VIRTUES:
      return {
        ...state,
        virtues: action.payload
      };
    case VirtueConstants.GET_VIRTUE:
      return {
        ...state,
        currentVirtue: action.payload
      };
    case VirtueConstants.CREATE_VIRTUE:
      return {
        ...state,
        virtues: [...state.virtues, action.payload]
      };
    case VirtueConstants.DELETE_VIRTUE:
      return {
        ...state,
        virtues: state.virtues.filter(v => v.virtueId !== action.payload)
      };
    case VirtueConstants.SET_CURRENT_VIRTUE:
      return {
        ...state,
        currentVirtue: action.payload
      };
    default:
      return state;
  }
};
