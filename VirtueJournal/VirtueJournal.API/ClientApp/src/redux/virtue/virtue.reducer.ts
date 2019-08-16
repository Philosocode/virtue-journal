import { Virtue } from "./virtue.types";

export const virtueReducer = (state: Virtue[] = [], action: any) => {
  switch (action.type) {
    // case VirtueActionTypes.fetchVirtues:
    //   return action.payload;
    default:
      return state;
  }
};
