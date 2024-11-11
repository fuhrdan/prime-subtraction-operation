//*****************************************************************************
//** 2601. Prime Subtraction Operation    leetcode                           **
//*****************************************************************************

#define MAX_N 1000

int* linear_sieve_of_eratosthenes(int n, int* prime_count) {
    int* spf = (int*)malloc((n + 1) * sizeof(int));  
    int* primes = (int*)malloc((n + 1) * sizeof(int));  
    for (int i = 0; i <= n; i++) {
        spf[i] = -1;
    }
    *prime_count = 0;

    for (int i = 2; i <= n; i++) {
        if (spf[i] == -1) {
            spf[i] = i;
            primes[(*prime_count)++] = i;
        }
        for (int j = 0; j < *prime_count && primes[j] <= spf[i] && i * primes[j] <= n; j++) {
            spf[i * primes[j]] = primes[j];
        }
    }
    free(spf);
    return primes;
}

int find_prev_prime(int* primes, int prime_count, int value) {
    int left = 0, right = prime_count - 1;
    while (left <= right) {
        int mid = left + (right - left) / 2;
        if (primes[mid] < value) {
            left = mid + 1;
        } else {
            right = mid - 1;
        }
    }
    return (right >= 0) ? primes[right] : -1;
}

bool primeSubOperation(int* nums, int numsSize) {
    int prime_count;
    int* primes = linear_sieve_of_eratosthenes(MAX_N - 1, &prime_count);

    for (int i = 0; i < numsSize; i++) {
        int target = (i - 1 >= 0) ? nums[i] - nums[i - 1] : nums[i];
        int prev_prime = find_prev_prime(primes, prime_count, target);
        if (prev_prime != -1) {
            nums[i] -= prev_prime;
        }
        if (i - 1 >= 0 && nums[i - 1] >= nums[i]) {
            free(primes);
            return false;
        }
    }

    free(primes);
    return true;
}