import { type ClassValue, clsx } from "clsx";
import { twMerge } from "tailwind-merge";
import { NFTStorage } from "nft.storage";
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export const storeAsset = async (data: FileList | File[] | null) => {
  if (!data) {
    return null;
  }
  const ipfsStorage = new NFTStorage({
    token: process.env.NEXT_PUBLIC_IPFS_API_KEY || "",
  });

  return ipfsStorage.storeDirectory(data);
};
