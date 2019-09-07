import React from "react";

interface Props {
  key: number,
  createdAt: Date,
  lastEdited: Date,
  title: string,
  description: string,
  starred: boolean
}

export const EntryItem = (props: Props): JSX.Element => {
  const { createdAt, lastEdited, title, description, starred } = props;

  return (
    <div className="c-entry">
      
    </div>
  )
}