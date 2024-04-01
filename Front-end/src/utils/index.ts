import { CATEGORY } from "@/data";
import { Collection, NFT, User } from "@/types";
import { faker } from "@faker-js/faker";
faker.seed(5);
const categories = [
  CATEGORY.ART,
  CATEGORY.AUDIO,
  CATEGORY.DESIGN,
  CATEGORY.EBOOK,
  CATEGORY.PHOTOGRAPHY,
  CATEGORY.THREE_D,
  CATEGORY.TICKET,
  CATEGORY.VIDEO,
];
const generateDummyAssets = () => {
  const dummyData = [];
  for (let i = 0; i < 10; i++) {
    const category = categories[Math.floor(Math.random() * categories.length)];
    const data: NFT = {
      tokenId: i + 1,
      name: faker.word.noun(),
      description: faker.lorem.sentence(),
      image: !(category === CATEGORY.VIDEO)
        ? faker.image.urlPicsumPhotos()
        : undefined,
      audio:
        category === CATEGORY.AUDIO
          ? "https://bafkreicmqfsldedmrvjsmsc57oy6r7ojugnswjj3wwcgmctp2hyiqgzkp4.ipfs.nftstorage.link/"
          : undefined,
      video:
        category === CATEGORY.VIDEO
          ? "https://bafybeibfb7hbpkzgm7cguy3eidp4hm7cmmiqpm5cuoucjqpzu7xkr6t2ba.ipfs.nftstorage.link/"
          : undefined,
      likes: faker.number.int({ min: 0, max: 100 }),
      liked: faker.datatype.boolean(Math.random()),
      category: category,
      price:
        i % 3 === 0
          ? faker.number
              .float({ min: 0.00001, max: 1, fractionDigits: 4 })
              .toString()
          : undefined,
      royalty: faker.number.int({ min: 1, max: 10 }),
      collection: {
        id: faker.number.int(),
        avatar: faker.image.avatar(),
        name: faker.word.noun(),
      },
      creator: {
        username: faker.internet.userName(),
        avatar: faker.image.avatar(),
        publicAddress: `0x${faker.finance.bitcoinAddress()}`,
      },
      owner: {
        username: faker.internet.userName(),
        avatar: faker.image.avatar(),
        publicAddress: `0x${faker.finance.bitcoinAddress()}`,
      },
      auction:
        i % 3 === 0
          ? undefined
          : {
              auctionId: faker.number.int(),
              auction_end: new Date(faker.date.future()).getTime(),
              highest_bid: faker.number
                .float({ min: 0.00001, max: 1, fractionDigits: 4 })
                .toString(),
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
        username: faker.internet.userName(),
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
      username: faker.internet.userName(),
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
