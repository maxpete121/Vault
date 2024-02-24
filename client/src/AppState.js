import { reactive } from 'vue'
import { Keeps } from './models/Keeps.js'
import { Account } from './models/Account.js'
import { Vaults } from './models/Vaults.js'
import { Tags } from './models/Tags.js'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
/**@type {Keeps[]} */
  allKeeps: [],
/**@type {Keeps} */
  activeKeep: null,
/**@type {Account} */
  activeUserProfile: {},
/**@type {Vaults[]} */
  userVaults: [],
/**@type {Keeps[]} */
  userKeeps: [],
/**@type {Vaults} */
  activeVault: {},
/**@type {Keeps[]} */
  vaultsKeeps: [],
/**@type {Vaults[]} */
  yourVaults: [],
/**@type {Vaults[]} */
  privateVaults: [],
/**@type {Tags[]} */
  activeTag: [],

  totalViews: null,
/**@type {Keeps} */
  mostViewed: {},
})
