import React from "react";
import CollectionCard from "../CollectionCard";

type Props = {};

const UserCollections = (props: Props) => {
  return (
    <div className="grid grid-cols-1  md:grid-cols-2 lg:grid-cols-3 justify-center items-center gap-5">
      {Array.from({ length: 6 }).map((_, index) => (
        <CollectionCard key={index} />
      ))}
    </div>
  );
};

export default UserCollections;
