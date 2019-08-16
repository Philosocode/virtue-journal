import { Entry } from "./entry.types";

export const entryReducer = (state: Entry[] = [], action: any) => {
  switch (action.type) {
    // case VirtueActionTypes.fetchVirtues:
    //   return action.payload;
    default:
      return state;
  }
};
