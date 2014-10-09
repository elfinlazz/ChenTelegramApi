#include <stdint.h>

#pragma once

class PQSolver
{
public:
    PQSolver();

    ~PQSolver();

    void solvePQ(uint64_t, uint32_t &, uint32_t &);
};