import { Entry, EntryState, EntryConstants, EntryAction } from "./entry.types";

const initialState: EntryState = {
  currentEntry: undefined,
  entries: []
};

export const entryReducer = (state = initialState, action: EntryAction) => {
  switch (action.type) {
    case EntryConstants.GET_ENTRIES_FOR_VIRTUE:
      return {
        ...state,
        entries: action.payload
      };
    case EntryConstants.GET_ENTRY:
      return {
        ...state,
        currentEntry: action.payload
      };
    case EntryConstants.CREATE_ENTRY:
      return {
        ...state,
        entries: [...state.entries, action.payload]
      };
    default:
      return state;
  }
}; 
