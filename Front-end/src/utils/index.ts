import { Collection, NFT, User } from "@/types";
import { faker } from "@faker-js/faker";
faker.seed(5);

const generateDummyAssets = () => {
  const dummyData = [];
  for (let i = 0; i < 10; i++) {
    const data: NFT = {
      tokenId: i + 1,
      name: faker.word.noun(),
      description: faker.lorem.sentence(),
      image: faker.image.urlPicsumPhotos(),
      likes: faker.number.int({ min: 0, max: 100 }),
      category: faker.word.sample(),
      price: faker.number
        .float({ min: 0.00001, max: 1, fractionDigits: 4 })
        .toString(),
      royalty: faker.number.int({ min: 1, max: 10 }),
      collection: {
        id: faker.number.int(),
        avatar: faker.image.avatar(),
        name: faker.word.noun(),
      },
      creator: {
        userName: faker.internet.userName(),
        avatar: faker.image.avatar(),
        publicAddress: `0x${faker.finance.bitcoinAddress()}`,
      },
      owner: {
        userName: faker.internet.userName(),
        avatar: faker.image.avatar(),
        publicAddress: `0x${faker.finance.bitcoinAddress()}`,
      },
      auction: {
        auctionId: faker.number.int(),
        auction_end: new Date(faker.date.future()).getTime(),
      },
      transactionHash: `0x${faker.finance.litecoinAddress()}`,
    };
    dummyData.push(data);
  }
  return dummyData;
};

export const assets = generateDummyAssets();

const generateDummyCollections = () => {
  const dummyData = [];
  for (let i = 0; i < 10; i++) {
    const data: Collection = {
      id: (i + 1).toString(),
      name: faker.word.noun(),
      description: faker.lorem.sentence(),
      avatar: faker.image.url(),
      creator: {
        userName: faker.internet.userName(),
        avatar: faker.image.avatar(),
        publicAddress: `0x${faker.finance.bitcoinAddress()}`,
      },
      volume: faker.number
        .float({ min: 1, max: 20, fractionDigits: 4 })
        .toString(),
      items: faker.number.int({ min: 5, max: 50 }),
      floor_price: faker.number
        .float({ min: 1, max: 20, fractionDigits: 4 })
        .toString(),
      latest_price: faker.number
        .float({ min: 1, max: 20, fractionDigits: 4 })
        .toString(),
      images: [
        faker.image.urlPicsumPhotos(),
        faker.image.urlPicsumPhotos(),
        faker.image.urlPicsumPhotos(),
        faker.image.urlPicsumPhotos(),
      ],
    };
    dummyData.push(data);
  }
  return dummyData;
};

export const collections = generateDummyCollections();

const generateDummyUsers = () => {
  const dummyData = [];
  for (let i = 0; i < 10; i++) {
    const data: User = {
      userName: faker.internet.userName(),
      avatar: faker.image.avatar(),
      publicAddress: `0x${faker.finance.bitcoinAddress()}`,
      bio: faker.person.bio(),
      profile_background: faker.image.urlPicsumPhotos(),
      social_media: {
        facebook: faker.internet.url(),
        twitter: faker.internet.url(),
        youtube: faker.internet.url(),
        telegram: faker.internet.url(),
      },
      sales: faker.number.float({ min: 10, max: 30, fractionDigits: 4 }),
    };
    dummyData.push(data);
  }
  return dummyData;
};

export const users = generateDummyUsers();
