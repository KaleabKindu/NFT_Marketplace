import Image, { ImageProps } from "next/image";
import { useState } from "react";

interface CustomImageProps extends Omit<ImageProps, "src"> {
  src: string;
  fallbackSrc?: string;
  alt: string;
}

const CustomImage = ({
  src,
  alt,
  fallbackSrc = "/image-placeholder.png",
  ...props
}: CustomImageProps) => {
  const [imgSrc, setImgSrc] = useState(src);

  const handleError = () => {
    setImgSrc(fallbackSrc);
  };
  return <Image src={imgSrc} alt={alt} onError={handleError} {...props} />;
};

export default CustomImage;
