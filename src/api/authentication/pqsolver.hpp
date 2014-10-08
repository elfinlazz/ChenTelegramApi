#include <stdint.h>

#pragma once

class PQSolver
{
public:
    PQSolver();

    ~PQSolver();

    void solvePQ(uint64_t, uint64_t*, uint64_t*);
};