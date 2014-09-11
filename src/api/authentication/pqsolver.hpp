#include <stdint.h>

#pragma once

class PQSolver
{
public:
    PQSolver();

    ~PQSolver();

    void solvePQ(int64_t, int64_t*, int64_t*);
};