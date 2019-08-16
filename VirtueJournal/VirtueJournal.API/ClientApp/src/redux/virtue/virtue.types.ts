export interface Virtue {
  virtueId: number,
  name: string,
  color: string,
  description: string,
  icon: string,
  createdAt: Date
}

export enum VirtueActionTypes {
  TEST = "TEST",
  WOW = "WOW"
};