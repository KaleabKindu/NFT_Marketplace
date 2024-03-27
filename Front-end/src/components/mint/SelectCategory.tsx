import { categories } from "@/data";
import { Badge } from "../ui/badge";
import { useState } from "react";
import clsx from "clsx";
import { Button } from "../ui/button";

type Props = {
  onChange: (a: string) => void;
};

const SelectCategory = ({ onChange }: Props) => {
  const [active, setActive] = useState("");
  return (
    <div className="flex flex-wrap gap-2">
      {categories.map((category, index) => (
        <Button
          type="button"
          key={index}
          onClick={() => {
            setActive(category.value);
            onChange(category.value);
          }}
          size={"sm"}
          className="rounded-full px-4 py-0.5 text-sm"
          variant={active === category.value ? "default" : "outline"}
        >
          {category.name}
        </Button>
      ))}
    </div>
  );
};

export default SelectCategory;
