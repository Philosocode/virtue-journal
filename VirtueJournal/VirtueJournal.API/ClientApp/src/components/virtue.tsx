import React from "react";

interface Props {
  color: string,
  createdAt: Date,
  description: string,
  icon: string,
  name: string,
  virtueId: number
}

export const Virtue = (props: Props): JSX.Element => {
  const { color, createdAt, description, icon, name, virtueId  } = props;

  return (
    <div className="c-virtue">
      <h3 className="c-virtue__header">{virtueId} - {name}</h3>
      <p className="c-virtue__description">{description}</p>
      <p className="c-virtue__metadata">{color} - {icon} - {createdAt}</p>
    </div>
  )
}